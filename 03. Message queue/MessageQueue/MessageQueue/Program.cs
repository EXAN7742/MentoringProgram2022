
using Confluent.Kafka;
using MessageQueue;
using System.Net;
using System.Text.Json;

string bootstrapServers = "localhost:9092";
string topic = "ScannedFiles";
string groupId = "GroupScannedFiles";

string scannedFilesDir = "C:\\Temp\\ScannedFiles";
string processedFilesDir = "C:\\Temp\\ProcessedFiles";

Task taskCollecting = Task.Run(() => collectingFiles());

Task taskProcessing = Task.Run(() => processingFiles());

Console.ReadLine();

async Task collectingFiles()
{
    ProducerConfig config = new ProducerConfig
    {
        BootstrapServers = bootstrapServers,
        ClientId = Dns.GetHostName()
    };

    using (var producer = new ProducerBuilder<Null, string>(config).Build())
    {
        Console.WriteLine("Producing started");
        do
        {
            foreach (string fileFullName in Directory.EnumerateFiles(scannedFilesDir, "*.*"))
            {
                try
                {
                    foreach (FileMessage fileMessage in SplitFileToMessages(fileFullName))
                    {
                        var deliveryReport = await producer.ProduceAsync(topic, new Message<Null, string>
                        {
                            Value = JsonSerializer.Serialize(fileMessage)
                        });
                        Console.WriteLine($"delivered {fileFullName} part {fileMessage.MessageIndex} to: {deliveryReport.TopicPartitionOffset}");
                    }
                    File.Delete(fileFullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Producing error: {0}", fileFullName);
                }

            }
        }
        while(true);
             
     }
}

List<FileMessage> SplitFileToMessages(string fileFullName)
{
    List<FileMessage> result = new List<FileMessage>();
    
    byte[] fileData = File.ReadAllBytes(fileFullName);

    int messageSize = 50 * 1024; //by 50kB
    int numberOfMessages = (int) Math.Ceiling((decimal)fileData.Length / (decimal)messageSize);

    for (int messageIndex = 1; messageIndex <= numberOfMessages; messageIndex ++)
    {
        byte[] partialFileData = fileData.Skip((messageIndex - 1) * messageSize).Take(messageSize).ToArray();
        result.Add(new FileMessage(Path.GetFileName(fileFullName), Convert.ToBase64String(partialFileData), messageIndex, numberOfMessages));
    }

    return result;
}

void processingFiles()
{
    var config = new ConsumerConfig
    {
        GroupId = groupId,
        BootstrapServers = bootstrapServers,
        AutoOffsetReset = AutoOffsetReset.Earliest
    };

    using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
    {
        Console.WriteLine("Consuming started");
        consumerBuilder.Subscribe(topic);       
        ConsumeResult<Ignore, string> consumer;
        
        byte[] fileData = new byte[0];

        do
        {
            consumer = consumerBuilder.Consume(1000);
            if (consumer == null)
                continue;
            try
            {
                FileMessage fileMessage = JsonSerializer.Deserialize<FileMessage>(consumer.Message.Value);
                fileData = fileData.Concat(Convert.FromBase64String(fileMessage?.FileData)).ToArray();
                if (fileMessage?.MessageIndex == fileMessage?.NumberOfMessages)
                {
                    File.WriteAllBytes($"{processedFilesDir}\\{fileMessage?.FileName}", fileData);
                    fileData = new byte[0];
                }
                else
                {
                    continue;
                }
                
                Console.WriteLine("Consumed from queue: {0}", fileMessage?.FileName);
            }
            catch
            {
                Console.WriteLine("Skipped: {0}", consumer.Message.Value);
            }
        }
        while (true); //!consumer.IsPartitionEOF);
    }
}

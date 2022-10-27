
using Confluent.Kafka;
using MessageQueue;
using System.Net;
using System.Text.Json;
using System.Text;

string bootstrapServers = "localhost:9092";
string topic = "ScannedFiles";
string groupId = "test_group";

string scannedFilesDir = "C:\\Temp\\ScannedFiles";
string processedFilesDir = "C:\\Temp\\ProcessedFiles";

//Task taskCollecting = Task.Run(() => collectingFiles());

//Task taskProcessing = Task.Run(() => processingFiles());

collectingFiles();
processingFiles();

Console.WriteLine("Press any key to exit");
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
        foreach (string fileFullName in Directory.EnumerateFiles(scannedFilesDir, "*.*"))
        {
            string fileData = Convert.ToBase64String(File.ReadAllBytes(fileFullName));
            FileMessage fileMessage = new(Path.GetFileName(fileFullName), fileData);
            await producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(fileMessage)
            });
            Console.WriteLine("Produced to queue: {0}", fileFullName);
        }        
        //producer.Flush();
     }
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
        consumerBuilder.Subscribe(topic);       

        ConsumeResult<Ignore, string> consumer;

        do
        {
            consumer = consumerBuilder.Consume(2000);
            if (consumer == null)
                break;
            try
            {
                FileMessage fileMessage = JsonSerializer.Deserialize<FileMessage>(consumer.Message.Value);
                byte[] fileData = Convert.FromBase64String(fileMessage.FileData);
                File.WriteAllBytes(String.Format($"{processedFilesDir}\\{fileMessage.FileName}"), fileData);

                Console.WriteLine("Consumed from queue: {0}", fileMessage.FileName);
            }
            catch
            {
                Console.WriteLine("Skipped: {0}", consumer.Message.Value);
            }   
        }
        while (!consumer.IsPartitionEOF);
    }
}

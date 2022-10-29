using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue
{
    internal class FileMessage
    {
        public string FileName { get; set; }
        public string FileData { get; set; }
        public int MessageIndex { get; set; }
        public int NumberOfMessages { get; set; }
        public FileMessage(string fileName, string fileData, int messageIndex,  int numberOfMessages)
        {
            FileName = fileName;
            FileData = fileData;
            MessageIndex = messageIndex;
            NumberOfMessages = numberOfMessages;
        }
    }
}

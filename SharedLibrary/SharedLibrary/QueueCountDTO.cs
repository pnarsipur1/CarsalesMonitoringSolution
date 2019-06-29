using System;

namespace SharedLibrary
{
    public class QueueCountDTO
    {
        public int MessageCount { get; set; }
        public string QueueName { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

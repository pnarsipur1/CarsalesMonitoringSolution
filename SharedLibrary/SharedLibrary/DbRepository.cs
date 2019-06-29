using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SharedLibrary
{
    public class DbRepository
    {
        public void InsertMessageCountIntoQueueHistory(string connectionString,
           QueueCountDTO queueCountDTO)
        {   
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                var sql = $"insert into QueueHistory ([MessageCount], [QueueName], [CreatedAt]) values({queueCountDTO.MessageCount},{queueCountDTO.QueueName}, {queueCountDTO.CreatedAt})";
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<QueueCountDTO> GetMessageCountFromQueueHistory(string connectionString, 
            DateTime startDate, 
            DateTime endDate, 
            string queueName)
        {
            List<QueueCountDTO> listQueueCount = new List<QueueCountDTO>();


            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                var sql = $"select MessageCount, QueueName, CreatedAt from QueueHistory where QueueName = '{queueName}' and CreatedAt between '{startDate}' and '{endDate}'";
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listQueueCount.Add(new QueueCountDTO()
                        {
                            MessageCount = Convert.ToInt32(reader[0]),
                            QueueName = Convert.ToString(reader[1]),
                            CreatedAt = Convert.ToDateTime(reader[2]),
                        });                        
                    }
                }
            }

            return listQueueCount;
        }
    }
}

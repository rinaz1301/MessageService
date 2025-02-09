using MessageService.Models;
using Microsoft.Data.SqlClient;

namespace MessageService.Repository
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
        Task<IEnumerable<Message>> GetMessages(DateTime time);
    }
    public class MessageRepository : IMessageRepository
    {
        private readonly string _connectionString;

        public MessageRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection");
        }
        public async Task AddMessage(Message message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string insert = @"INSERT INTO Messages (text, timestap, sequencenumber)
                                    VALUES (@text, @time, @sequencenumber)";
                using(var command = new SqlCommand(insert, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("text", message.Text);
                    command.Parameters.AddWithValue("time", DateTime.UtcNow);
                    command.Parameters.AddWithValue("sequencenumber", message.SequenceNumber);
                    await command.ExecuteNonQueryAsync();
                }
            }
           
        }

        public async Task<IEnumerable<Message>> GetMessages(DateTime time)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var messages = new List<Message>();
                string insert = @"SELECT * FROM Messages WHERE TimeStap >= @time";
                using (var command = new SqlCommand(insert, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("time", time.AddMinutes(-10));
                    using(var reader = await command.ExecuteReaderAsync())
                    {
                        while(reader.Read())
                        {
                            Message message = new Message()
                            {
                                Text = reader.GetString(1),
                                Timestamp = reader.GetDateTime(2),
                                SequenceNumber = reader.GetInt32(3),
                            };
                            messages.Add(message);
                        }
                    }
                }
                return messages;
            }
        }
    }
}

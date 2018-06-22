using System;
namespace wall.Models
{
    public class Comment : BaseEntity
    {
        public int id { get; set; }
        public int message_id { get; set; }
        public int user_id { get; set; }
        public string comment { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
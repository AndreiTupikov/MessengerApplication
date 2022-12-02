using System;
using System.ComponentModel.DataAnnotations;

namespace MessengerApplication.Models
{
    public class Message
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "*the topic length must be no more than 30 characters")]
        public string Topic { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "*message body is requred")]
        public string Body { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "*receiver is requred")]
        public int Sender_Id { get; set; }
        public int Receiver_Id { get; set; }
        public bool NotReaded { get; set; }
        public bool Noticated { get; set; }
        public Message()
        {
            Date = DateTime.Now;
            NotReaded = true;
            Noticated = false;
        }
    }
}
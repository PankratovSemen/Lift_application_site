using Lift_application.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Lift_application.Models
{
    public class SenderEmailModel
    {
        [Key]
        public int Id { set; get; }
       

        public string Title { set; get; }
        public string Subject { set; get; }
        [DataType(DataType.MultilineText)]
        public string TextMessage { set; get; }
        public string StatusSend { set; get; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictouristAPI.Areas.Admin
{
    public class Friend
    {
        [Key]
        public Guid Id { get; set; }
        //[ForeignKey("UserId")]
        public string FirstFriendId { get; set; }
        //[ForeignKey("UserId")]
        public string SecondFriendId { get; set; }
        public byte RelationType { get; set; } // 1 -- первый отправил заявку второму, -1 -- второй, 0 -- друзья.
    }
}

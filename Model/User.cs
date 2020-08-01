
namespace Hostman.Model
{
    public class User
    {
        public const int NICKNAME_MIN_LENGTH = 3;
        public const int NICKNAME_MAX_LENGTH = 16;

        public string Nickname { get; set; }
    }
}

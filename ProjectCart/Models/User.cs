using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProjectCart.Models
{
    
    public class User()
    {
    public int Id { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }

        public static string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}


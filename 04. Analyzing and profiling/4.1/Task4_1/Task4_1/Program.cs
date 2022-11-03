// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

Random random = new Random();

byte[] salt = new byte[16];  
for(int i = 0; i < salt.Length; i++)
{
    salt[i] = (byte) random.Next(0,255);
}

GeneratePasswordHashUsingSalt("MyNewPassword", salt);

Console.ReadKey();

string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{

    var iterate = 1000000;
    var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
    byte[] hash = pbkdf2.GetBytes(20);

    byte[] hashBytes = new byte[36];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 20);

    var passwordHash = Convert.ToBase64String(hashBytes);

    return passwordHash;

}
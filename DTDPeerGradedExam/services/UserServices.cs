using models;
using utilities;

namespace services
{
    public class UserServices : FileService
    {
        private const string FileName = "User.json";

        public List<User> LoadUsers()
        {
            return LoadFile<User>(FileName);
        }

        public User? GetUserByID(int id)
        {
            return LoadUsers().FirstOrDefault(u => u.ID == id); 
        }

        public void AddUsers(User user)
        {
            var users = LoadUsers();
            users.Add(user);
            SaveFile<User>(FileName, users);
        }

        public void UpdateUsers(User user)
        {
            var users = LoadUsers();
            var existing = users.FirstOrDefault(u => u.ID == user.ID);

            if (existing == null)
            {
                return;
            }

            existing.Username = user.Username;
            existing.Password = user.Password;
            existing.Role = user.Role;
            existing.Email = user.Email;
            existing.PhoneNumber = user.PhoneNumber;
            existing.Address = user.Address;

            SaveFile<User>(FileName, users);      
        }

        public void DeleteUsers(int id)
        {
            var users = LoadUsers();
            var userToDelete = users.FirstOrDefault(u => u.ID == id);
            if (userToDelete == null)
            {
                return;
            }
            users.Remove(userToDelete);
            SaveFile<User>(FileName, users);
        }
    }
}
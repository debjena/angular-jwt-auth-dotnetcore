using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace angular_jwt_auth_dotnetcore.Models {
    public class UserDbContext : DbContext {
        public UserDbContext (DbContextOptions<UserDbContext> options) : base (options) {
            if (this.Users.Count () == 0) {
                this.Users.Add (new User () { UserName = "alpha", Password = "beta" });
                this.SaveChanges ();
            }
        }

        public DbSet<User> Users { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UsersPasswordStore.Domain.Model
{
    public class UsersPassword
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? App { get; set; }
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }

        [JsonIgnore]
        public string? DecryptedPassword { get; set; }
    }
}


using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Login {get;set;}
        public string Password {get;set;}
        [JsonIgnore]
        public virtual LoginModel LoginModel {get;set;}
    }
}
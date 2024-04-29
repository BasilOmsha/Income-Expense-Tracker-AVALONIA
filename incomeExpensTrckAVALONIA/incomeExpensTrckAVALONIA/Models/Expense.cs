using MongoDB.Bson;
using Realms;

namespace incomeExpensTrckAVALONIA.Models
{
    public partial class Expense : IRealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [MapTo("day")]
        public string Day { get; set; }

        [MapTo("month")]
        public string Month { get; set; }

        [MapTo("year")]
        public string Year { get; set; }

        [MapTo("amount")]
        public double Amount { get; set; }

        [MapTo("category")]
        public string Category { get; set; }

        [MapTo("account")]
        public string Account { get; set; }

        [MapTo("location")]
        public string? Location { get; set; }

        [MapTo("latitude")]
        public string? Latitude { get; set; }

        [MapTo("longitude")]
        public string? Longitude { get; set; }

        [MapTo("note")]
        public string Note { get; set; }

        [MapTo("description")]
        public string? Description { get; set; }
    }
}

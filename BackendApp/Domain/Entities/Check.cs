using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Check
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public DateTime CheckDate { get; set; }
        public string State { get; set; }

        public void Update(Check check)
        {
            if(check.UserId>0) this.UserId = check.UserId;
            this.CheckDate = DateTime.Today;
            if(check.State != "") this.State = check.State;
        }

        private string UpdateState(Check check)
        {
            if(this.State == "UNDONE") return "DOING";
            else if(this.State == "DOING") return "DONE";
            else throw new ArgumentException("No se puede actualizar estado");
        }
    }
}
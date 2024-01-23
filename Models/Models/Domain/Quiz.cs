using Models.BaseModles.Domain.Base;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models.Modles.Domain
{
    public class Quiz : QuizeBase<Question>, IIdentity
    {
        public int Id { get; set; }
    }
}

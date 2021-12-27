using System.Collections.Generic;

namespace Model.Out
{
    public class PostCheckOut
    {
        List<RentalBasicInfoModel> Rents { get; set; }
        List<CheckBasicInfoModel> Checks { get; set; }
        public PostCheckOut(List<RentalBasicInfoModel> rents, List<CheckBasicInfoModel> checks)
        {
            this.Rents = rents;
            this.Checks = checks;
        }
    }
}
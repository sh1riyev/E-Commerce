using System;
namespace E_Commerce.Business.Helpers
{
	public enum EntityFilter{
        GetLastDayCreatedByAdmin=1,
        GetLastWeekCreatedByAdmin,
        GetLastMonthCreatedByAdmin,
        GetLastDayDeletedByAdmin,
        GetLastWeekDeletedByAdmin,
        GetLastMonthDeletedByAdmin,
        GetLastDayUpdatedByAdmin,
        GetLastWeekUpdatedByAdmin,
        GetLastMonthUpdatedByAdmin
    }
}


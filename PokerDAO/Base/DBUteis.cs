using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Comum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PokerDAO.Base
{
    public static class DBUteis
    {
        public static string ToDateTimePGSQL(DateTime dateTime) => dateTime.ToString("dd-MM-yyyy HH:mm:ss");

        public static string ToTimeStampPGSQL(TimeSpan dateTime) => dateTime.ToString();
    }
}

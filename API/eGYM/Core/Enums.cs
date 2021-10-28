﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public enum DataTypes
    {
        Int = 0,
        String = 1,
        Date = 2,
        Boolean = 3,
        Double = 4,
        Currency = 5
    }

    public enum GenreEnum
    {
        Male = 0,
        Female = 1
    }

    public enum InvoiceStatusEnum
    {
        Generated = 0,
        Paid = 5,
        Canceled = 10
    }

    public enum PaymentReversalStatusEnum
    {
        Opened = 0,
        Movimented = 5,
        Deffered = 10,
        Dismissed = 15,
        Canceled = 20
    }

    public enum RequestStatusEnum
    {
        Opened = 0,
        Deffered = 5,
        Dismissed = 10,
        Canceled = 15
    }

    public enum UserStateEnum
    {
        Inactive = 0,
        Active = 1,
    }
}
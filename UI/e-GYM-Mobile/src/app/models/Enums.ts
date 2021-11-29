export enum InvoiceStatusEnum {
    Generated = 0,
    Paid = 5,
    Canceled = 10
}

export enum PaymentReversalStatusEnum {
    Opened = 0,
    Movimented = 5,
    Deffered = 10,
    Dismissed = 15,
    Canceled = 20
}

export enum RequestStatusEnum {
    Opened = 0,
    Deffered = 5,
    Dismissed = 10,
    Canceled = 15
}

export enum PaymentTypeEnum {
    MONEY = 1,
    TICKET = 0,
    CREDIT_CARD = 2,
    CARD = 3,
}

export enum RequestCategoryEnum {
    Training = 1,
    Reversal = 2,
    Physical = 3
}
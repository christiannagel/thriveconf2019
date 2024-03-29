﻿namespace TPHWithFluentAPI
{
    public abstract class Payment
    {
        public int PaymentId { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}

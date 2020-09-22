using System;
using System.ComponentModel.DataAnnotations;

public class DateFromThePast : ValidationAttribute {
    public override bool IsValid(object date) {
        DateTime _date = (DateTime)date;
        return _date < DateTime.Now;
    }
}

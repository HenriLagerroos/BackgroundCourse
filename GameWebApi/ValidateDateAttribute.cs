using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

//To test this, change NewItem to item and send date through postman

public class ValidateDateAttribute : ValidationAttribute {

    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        Console.WriteLine("Mopi");
        Item item = (Item)validationContext.ObjectInstance;
        if (DateTime.Compare(item.CreationDate, DateTime.Now) > 0 ) {
            return new ValidationResult("Nope");
        }
        return ValidationResult.Success;
    }
}

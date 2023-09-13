namespace API.Extensions
{
  public static class DateTimeExtensions //
  {
    public static int CalculateAge(this DateOnly dob)
    {
      var today = DateOnly.FromDateTime(DateTime.Now);
      var age = today.Year - dob.Year;
      if (dob > today.AddYears(-age)) age--; // check if user pass his birthday this year
      return age;
    }
  }
}
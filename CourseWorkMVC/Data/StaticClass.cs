namespace CourseWorkMVC.Data
{
    public static class StaticClass
    {
        public static Account? CurrentAccount { get; set; }

        public static ApplicationDbContext? Context { get; set; }
    }
}

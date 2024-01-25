namespace Models.Models.Domain.Base
{
    /// <summary>
    /// Base model of Quiz. No id in the base class
    /// </summary>
    public class QuizeBase<T>
    {
        public bool IsEnabled { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ulong TimeLimitInMinutes { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public List<T> Questions { get; set; } = null!;

    }
}

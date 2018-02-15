namespace TestTask
{

    public class Job
    {
        public JobTypes JobType;

        public Job(JobTypes jobType)
        {
            this.JobType = jobType;
        }
    }

    public enum JobTypes 
    {
        Classic,
        Standout,
        Premium
    }
}
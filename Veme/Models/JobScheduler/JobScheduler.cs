using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veme.Models
{
    public class JobScheduler
    {
        public static void Start()
        {
            //ISchedulerFactory schedFact = StdSchedulerFactory.GetDefaultScheduler();//new StdSchedulerFactory();
            
            //construct a scheduler factory
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            //get a scheduler
            scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<PopuplateRepoJob>()
            .WithIdentity("myJob", "group1")
            .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00,01))
              .Build();

            //sched.ScheduleJob(job, trigger);
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
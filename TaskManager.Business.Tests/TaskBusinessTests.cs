using AutoMapper;
using NBench;
using System;
using System.Collections.Generic;
using TaskManager.BL;
using TaskManager.BusinessEntities;
using TaskManager.DAO;

namespace TaskManager.BL.Tests
{
    public class TaskBusinessLoadTest
    {
        public TaskBusinessLoadTest()
        {

        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            Mapper.Reset();
        }

        [PerfBenchmark(Description = "***** Result for GetAllParentTask *****",
                                                       NumberOfIterations = 2,
                                                       RunMode = RunMode.Throughput,
                                                       TestMode = TestMode.Measurement, SkipWarmups = false)]

        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetAllParentTask()
        {
            IEnumerable<ParentTaskViewModel> parentTasks;
            IRepositoryDAO<ProjectTask> taskRepository = new RepositoryDAO<ProjectTask>();
            IRepositoryDAO<ParentTask> parenttaskRepository = new RepositoryDAO<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);
            TaskBusinessBL taskBusiness = new TaskBusinessBL(taskRepository, taskbusiness);
            parentTasks = taskBusiness.GetAllParentTasksBL();
        }

        [PerfBenchmark(Description = "***** Result for GetTasks *****",
                                                      NumberOfIterations = 2,
                                                      RunMode = RunMode.Throughput,
                                                      TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetTasks()
        {
            IEnumerable<TaskViewModel> tasks;
            IRepositoryDAO<ProjectTask> taskRepository = new RepositoryDAO<ProjectTask>();
            IRepositoryDAO<ParentTask> parenttaskRepository = new RepositoryDAO<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);
            TaskBusinessBL taskBusiness = new TaskBusinessBL(taskRepository, taskbusiness);
            tasks = taskBusiness.GetAllTasksBL();
        }

        [PerfBenchmark(Description = "***** Result for GetTask By ID *****",
                                                      NumberOfIterations = 2,
                                                      RunMode = RunMode.Throughput,
                                                      TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetTaskById()
        {
            TaskViewModel task;
            IRepositoryDAO<ProjectTask> taskRepository = new RepositoryDAO<ProjectTask>();
            IRepositoryDAO<ParentTask> parenttaskRepository = new RepositoryDAO<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);
            TaskBusinessBL taskBusiness = new TaskBusinessBL(taskRepository, taskbusiness);
            task = taskBusiness.GetByIdBL(1);
        }

        [PerfBenchmark(Description = "***** Result for AddTask *****",
                                                        NumberOfIterations = 2,
                                                        RunMode = RunMode.Throughput,
                                                        TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkSaveTask()
        {
            TaskViewModel task = new TaskViewModel
            {
                TaskName = "Add Task from nbench",
                StartDate = Convert.ToDateTime("12/12/2018"),
                ParentTaskId = 1,
                Priority = 15,
                EndDate = Convert.ToDateTime("12/12/2018"),
                TaskId = 0,
                ParentTaskName = string.Empty
            };

            IRepositoryDAO<ProjectTask> taskRepository = new RepositoryDAO<ProjectTask>();
            IRepositoryDAO<ParentTask> parenttaskRepository = new RepositoryDAO<ParentTask>();
            IParentTaskBusiness taskbusiness = new ParentTaskBusiness(parenttaskRepository);
            TaskBusinessBL taskBusiness = new TaskBusinessBL(taskRepository, taskbusiness);
            taskBusiness.SaveBL(task);
        }

        [PerfCleanup]
        public void Cleanup()
        {
        }
    }
}

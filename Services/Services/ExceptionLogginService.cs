using AutoMapper;
using Data.Entities;
using Data.IRepository;
using Data.Repository.GenericRepository;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Services.Generic;
using Services.IServices;
using Shared.Common;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services.Services
{
    /// <summary>
    /// Exception logging service
    /// </summary>
    public class ExceptionLogginService : IExceptionLogginService//: GenericService<ExceptionModel, ExceptionEntity>, IExceptionLogginService
    {

        //private static readonly Lazy<ExceptionLogginService> lazy = new Lazy<ExceptionLogginService>(() => new ExceptionLogginService());
        //public static ExceptionLogginService Instance => lazy.Value;

        protected readonly IGenericDataRepository<ExceptionEntity> repository;
        protected readonly IMapper mapper;

        public ExceptionLogginService(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope()) // this will use `IServiceScopeFactory` internally
            {
                repository = scope.ServiceProvider.GetService<IGenericDataRepository<ExceptionEntity>>();
                mapper = scope.ServiceProvider.GetService<IMapper>();
            }

            ExceptionLoggerExtentionMetod.service = this;
        }


        /// <summary>
        /// Save Exception details.
        /// </summary>
        /// <param name="model">exception model value for save.</param>
        /// <returns>Is successfullt save or not in terms of True/False</returns>
        public bool SaveException(ExceptionModel model)
        {

            try
            {
                LogExceptionInFile(model);

                if (model != null)
                {
                    var entity = mapper.Map<ExceptionEntity>(model);
                    repository.Add(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogExceptionInFile(ex);
                return false;
            }
        }


        public void LogExceptionInFile(object model)
        {

            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("./ExceptionLoggin.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }

            writer.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented) + "\n\n\n\n\n\n\n" + DateTime.UtcNow.GetLocal()
                .ToString());

            writer.Close();
            ostrm.Close();



        }

        /// <summary>
        /// Save Exception details.
        /// </summary>
        /// <param name="model">exception value for save.</param>
        /// <returns>Is successfullt save or not in terms of True/False</returns>
        public bool SaveException(Exception model)
        {
            try
            {
                //LogExceptionInFile(model);

                if (model != null)
                {
                    ExceptionEntity exe = new ExceptionEntity();
                    exe.CreatedDate = DateTime.UtcNow.GetLocal();
                    exe.ModifiedDate = DateTime.UtcNow.GetLocal();
                    exe.IsActive = true;
                    exe.IsDeleted = false;
                    exe.StackTrace = model.StackTrace;
                    exe.FunctionName = model.TargetSite.Name;
                    exe.SourceFileName = model.TargetSite.ReflectedType.FullName;
                    exe.APISource = model.Source;
                    exe.ExceptionType = model.GetType().Name;
                    exe.ExceptionMessage = model.ToString();
                    exe.ModelValues = string.Empty;

                    repository.Add(exe);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogExceptionInFile(ex);
                return false;
            }
        }
    }

    /// <summary>
    /// Exception logging extention method
    /// </summary>
    public static class ExceptionLoggerExtentionMetod
    {
        public static IExceptionLogginService service;

        /// <summary>
        /// Log the exception in DB
        /// </summary>
        /// <param name="model"></param>
        public static void Log(this Exception model)
        {
            if (model != null)
            {
                service?.SaveException(model);
            }

        }

        /// <summary>
        /// Log the exception model in DB
        /// </summary>
        /// <param name="model"></param>
        public static void Log(this ExceptionModel model)
        {
            if (model != null)
            {
                service?.SaveException(model);
            }

        }

        /// <summary>
        /// Get exception model from exception
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ExceptionModel GetExceptionModel(this Exception model)
        {
            ExceptionModel exe = new ExceptionModel();
            if (model != null)
            {

                exe.CreatedDate = DateTime.UtcNow.GetLocal();
                exe.ModifiedDate = DateTime.UtcNow.GetLocal();
                exe.IsActive = true;
                exe.IsDeleted = false;
                exe.StackTrace = model.StackTrace;
                exe.FunctionName = model.TargetSite.Name;
                exe.SourceFileName = model.TargetSite.ReflectedType.FullName;
                exe.APISource = model.Source;
                exe.ExceptionType = model.GetType().Name;
                exe.ExceptionMessage = model.ToString();

            }
            return exe;
        }

    }
}

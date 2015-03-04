﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MegaCityOne.Mvc
{
    /// <summary>
    /// This attribute leverage MegaCityOne's Judge security for MVC 
    /// applications. The rule to be advised is mandatory. Note that the Users and 
    /// Roles properties from the base AuthorizeAttribute are ignored by this
    /// specialization of AuthorizeAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class JudgeAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// The rule to be advised by the Judge upon authorization request.
        /// </summary>
        public string Rule { get; set; }

        /// <summary>
        /// Creates an instance of a JudgeAuthorizeAttribute.
        /// </summary>
        public JudgeAuthorizeAttribute()
        {
            this.Rule = null;
        }

        /// <summary>
        /// Calls the Advise method of the Judge returned by the 
        /// Dispatcher.Dispatch() method with the Rule property as the first 
        /// Advise parameter and the given httpContext as the second Advise 
        /// parameter.
        /// </summary>
        /// <param name="httpContext">The http context of the current 
        /// controller method call. This parameter will be passed to the 
        /// Judge.Advise method as the first and only argument.</param>
        /// <returns>The Judge.Advise result.</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Judge judge = Dispatcher.Current.Dispatch();
            bool advisal = judge.Advise(this.Rule, httpContext);
            Dispatcher.Current.Returns(judge);
            return advisal;
        }
    }
}

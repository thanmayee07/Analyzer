﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    public class ParsedDLLFiles
    {
        public List<ParsedClass> classObjList = new();
        public List<ParsedInterface> interfaceObjList = new();
        public ParsedDLLFiles(List<string> paths) // path of dll files
        {
            // take the input of all the dll files
            // it merge the all the ParsedNamespace
            foreach(var path in paths) 
            {
                Assembly assembly = Assembly.LoadFrom(path);

                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.Namespace != null)
                        {
                            if (type.Namespace.StartsWith("System.") || type.Namespace.StartsWith("Microsoft."))
                            {
                                continue;
                            }

                            if (type.IsClass)
                            {
                                ParsedClass classObj = new ParsedClass(type);
                                classObjList.Add(classObj);
                            }
                            else if (type.IsInterface)
                            {
                                ParsedInterface interfaceObj = new ParsedInterface(type);
                                interfaceObjList.Add(interfaceObj);
                            }
                        }
                        else
                        {
                            // code written outside all namespaces may have namespace as null
                            // will handle later
                        }
                    }
                }
            }

        }

    }
}


// it will call the constructor of the ParsedNamespace for each dll file
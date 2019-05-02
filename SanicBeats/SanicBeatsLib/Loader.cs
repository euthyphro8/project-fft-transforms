﻿using System;
using Exception = System.Exception;
using java.lang;
using java.net;

namespace SanicBeatsLib
{
    public static class Loader
    {

        public static Type FftType;
        public static object FftInstance;

        public static void LoadJavaLib()
        {
            URLClassLoader loader = new URLClassLoader(new URL[]{
                new URL("file:rsc/SanicBeatsLib.jar")
            });
            try
            {
                // load the Class
                Class fftClass = Class.forName("com.sanicbeats.FFT", true, loader);


                //Create a object via C# reflection
                FftType = ikvm.runtime.Util.getInstanceTypeFromClass(fftClass);
                object obj = FftType.GetConstructor(new Type[] { typeof(double), typeof(double) }).Invoke(new object[] { 1.0, 1.0 });
                Console.WriteLine(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static byte[] Invoke(string method, byte[] data)
        {
            object result = FftType.GetMethod(method, new Type[] { typeof(byte[]) }).Invoke(FftInstance, new object[] { data });
            if (result is byte[] resultData)
                return resultData;
            throw new Exception("The invokation of a java object failed.");
        }

    }
}

using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace Assets._1_Solution.Scripts.Helpers
{
    public class LogWatcherHelper : IDisposable
    {
        public string Content { get; set; }
        private bool IsDisposed { get; set; }

        private readonly Stream stream;
        private readonly StreamReader reader;
        private readonly CultureInfo culture;

        public LogWatcherHelper(Stream _stream)
        {
            reader = new StreamReader(stream = _stream);
        }

        public void Read()
        {
            if (IsDisposed)
                throw new ObjectDisposedException("stream");

            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                Content = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            stream.Dispose();
            reader.Dispose();
        }

        public static LogWatcherHelper FromFile(string path)
        {
            Stream stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite);

            return new LogWatcherHelper(stream);
        }
    }
}

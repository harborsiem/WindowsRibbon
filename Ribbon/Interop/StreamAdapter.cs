//*****************************************************************************
//
//  File:       StreamAdapter.cs
//
//  Contents:   Helper class that wraps a .NET stream class as a COM IStream
//
//*****************************************************************************

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace RibbonLib.Interop
{
    /// <summary>
    /// Helper class that wraps a .NET stream class as a COM IStream
    /// </summary>
    public class StreamAdapter : IStream
    {
        private Stream _stream;

        /// <summary>
        /// Initializes a new instance of the StreamAdapter
        /// </summary>
        /// <param name="stream"></param>
        public StreamAdapter(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            _stream = stream;
        }

        #region IStream Members

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="streamCopy"></param>
        public void Clone(out IStream streamCopy)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="flags"></param>
        public void Commit(int flags)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="targetStream"></param>
        /// <param name="bufferSize"></param>
        /// <param name="buffer"></param>
        /// <param name="bytesWrittenPtr"></param>
        public void CopyTo(IStream targetStream, long bufferSize, IntPtr buffer, IntPtr bytesWrittenPtr)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="byteCount"></param>
        /// <param name="lockType"></param>
        public void LockRegion(long offset, long byteCount, int lockType)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Write the Int32 value of the total number of bytes read into the buffer to bytesReadPtr
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="bytesReadPtr"></param>
        public void Read(byte[] buffer, int bufferSize, IntPtr bytesReadPtr)
        {
            int val = _stream.Read(buffer, 0, bufferSize);
            if (bytesReadPtr != IntPtr.Zero)
            {
                Marshal.WriteInt32(bytesReadPtr, val);
            }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        public void Revert()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Write the new Int64 position of the stream to newPositionPtr
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <param name="newPositionPtr"></param>
        public void Seek(long offset, int origin, IntPtr newPositionPtr)
        {
            SeekOrigin begin;
            switch (origin)
            {
                case 0:
                    begin = SeekOrigin.Begin;
                    break;

                case 1:
                    begin = SeekOrigin.Current;
                    break;

                case 2:
                    begin = SeekOrigin.End;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("origin");
            }
            long val = _stream.Seek(offset, begin);
            if (newPositionPtr != IntPtr.Zero)
            {
                Marshal.WriteInt64(newPositionPtr, val);
            }
        }

        /// <summary>
        /// Set the length for the stream
        /// </summary>
        /// <param name="libNewSize"></param>
        public void SetSize(long libNewSize)
        {
            _stream.SetLength(libNewSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamStats"></param>
        /// <param name="grfStatFlag"></param>
        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG streamStats, int grfStatFlag)
        {
            streamStats = new System.Runtime.InteropServices.ComTypes.STATSTG();
            streamStats.type = 2;
            streamStats.cbSize = _stream.Length;
            streamStats.grfMode = 0;
            if (_stream.CanRead && _stream.CanWrite)
            {
                streamStats.grfMode |= 2;
            }
            else if (_stream.CanRead)
            {
                //streamStats.grfMode = streamStats.grfMode;
            }
            else
            {
                if (!_stream.CanWrite)
                {
                    throw new IOException("StreamObjectDisposed");
                }
                streamStats.grfMode |= 1;
            }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="byteCount"></param>
        /// <param name="lockType"></param>
        public void UnlockRegion(long offset, long byteCount, int lockType)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Write Int32 value bufferSize to bytesWrittenPtr
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="bytesWrittenPtr"></param>
        public void Write(byte[] buffer, int bufferSize, IntPtr bytesWrittenPtr)
        {
            _stream.Write(buffer, 0, bufferSize);
            if (bytesWrittenPtr != IntPtr.Zero)
            {
                Marshal.WriteInt32(bytesWrittenPtr, bufferSize);
            }
        }

        #endregion
    }
}

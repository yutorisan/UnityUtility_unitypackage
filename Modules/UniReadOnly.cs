﻿using System;

namespace UnityUtility.Modules
{
    /// <summary>
    /// 一度しか初期化できない値です。
    /// readonlyキーワードの代わりに使用します。
    /// </summary>
    /// <typeparam name="T">ラップする型</typeparam>
    public class UniReadOnly<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOverwriteIgnoreMode">上書きしようとしたときに例外が発生するのではなく無視するモードの有効無効</param>
        public UniReadOnly(bool isOverwriteIgnoreMode = false) => IsOverwriteIgnoreMode = isOverwriteIgnoreMode;

        ///// <summary>
        ///// 値を取得します。
        ///// </summary>
        public T Value { get; private set; }
        /// <summary>
        /// 初期化されているかどうかを取得します。
        /// </summary>
        public bool IsInitialized { get; private set; }
        /// <summary>
        /// 上書きしようとしたときに例外が発生するのではなく無視するモードの有効無効
        /// </summary>
        public bool IsOverwriteIgnoreMode { get; }

        /// <summary>
        /// 値を初期化します。このメソッドは一度しか呼ぶことができません。
        /// </summary>
        /// <param name="value">初期化する値</param>
        /// <exception cref="AlreadyInitializedException">複数回初期化しようとしたときにスローされます。</exception>
        public void Initialize(T value)
        {
            if (IsInitialized)
            {
                if (IsOverwriteIgnoreMode) return;
                else throw new AlreadyInitializedException();
            }
            IsInitialized = true;
            Value = value;
        }

        public override string ToString() => Value.ToString();
        public static implicit operator T(in UniReadOnly<T> readOnly) => readOnly.Value;
    }

    internal class AlreadyInitializedException : Exception
    {
        public AlreadyInitializedException() : base("すでに初期化されています。")
        {
        }
    }
}

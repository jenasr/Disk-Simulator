using System;


namespace YuGiOh {
    public struct PrePostExecutionEvents {
        public event Action OnPreExecute;
        public event Action OnPostExecute;

        public void InvokePre() {
            OnPreExecute?.Invoke();
        }
        public void InvokePost() {
            OnPostExecute?.Invoke();
        }
    }

    public struct PrePostExecutionEvents<T> {
        public event Action<T> OnPreExecute;
        public event Action<T> OnPostExecute;

        public void InvokePre(T t) {
            OnPreExecute?.Invoke(t);
        }
        public void InvokePost(T c) {
            OnPostExecute?.Invoke(c);
        }
    }
}
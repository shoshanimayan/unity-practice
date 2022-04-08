using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;
    private Task _t;
    private bool _faded;
    private CancellationTokenSource _cToken;

    private async Task FadeInOut(CancellationToken t = default)
    {
        if (!_faded)
        {
            while (_canvas.alpha > 0)
            {
                _canvas.alpha -= Time.deltaTime;
                if (t.IsCancellationRequested) {
                    return;
                }
                await Task.Yield();

            }
            

        }
        else
        {
            while (_canvas.alpha < 1)
            {
                _canvas.alpha += Time.deltaTime;
                if (t.IsCancellationRequested)
                {
                    return;
                }
                await Task.Yield();

            }
        }
        _faded = !_faded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _cToken != null)
        {
            
                _cToken.Cancel();
                _cToken.Dispose();
                _cToken = new CancellationTokenSource();
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && (_t == null||_t.IsCompleted))
        {
                if (_cToken == null)
                {
                    _cToken = new CancellationTokenSource();

                }
                _t = FadeInOut(_cToken.Token);
        }
    }
}

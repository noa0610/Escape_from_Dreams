using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [System.Serializable]
    private class SoundData
    {
        public SoundData(AudioClip audioClip)
        {
            this.audioClip = audioClip;
            playedTime = Time.realtimeSinceStartup;
        }

        public AudioClip audioClip;
        public float playedTime; //�Đ�����
    }

    //��x�Đ����Ă���A���Đ��o����܂ł̊Ԋu(�b)
    private float INTERVAL = 0.2f;

    //AudioSource�i�X�s�[�J�[�j�𓯎��ɖ炵�������̐������p��
    private AudioSource[] _seSources = new AudioSource[20];
    private AudioSource[] _bgmSources = new AudioSource[1];

    private Dictionary<string, SoundData> _seData = new Dictionary<string, SoundData>();
    private Dictionary<string, SoundData> _bgmData = new Dictionary<string, SoundData>();

    private void Awake()
    {
        Init();

        //AudioSource���������g�ɐ������Ĕz��Ɋi�[
        for (int i = 0; i < _seSources.Length; i++)
        {
            _seSources[i] = gameObject.AddComponent<AudioSource>();
        }

        for (int i = 0; i < _bgmSources.Length; i++)
        {
            _bgmSources[i] = gameObject.AddComponent<AudioSource>();
        }

        //���f�[�^�̓ǂݍ���
        var seClips = Resources.LoadAll<AudioClip>("SE");
        var bgmClips = Resources.LoadAll<AudioClip>("BGM");

        //���f�[�^�̊i�[
        foreach (var seClip in seClips)
        {
            var soundData = new SoundData(seClip);
            _seData.Add(seClip.name, soundData);
        }

        foreach (var bgmClip in bgmClips)
        {
            var soundData = new SoundData(bgmClip);
            _bgmData.Add(bgmClip.name, soundData);
        }
    }

    /// <summary>
    /// ���g�p��AudioSource(SE)���������A�擾����֐�
    /// </summary>
    /// <returns>���g�p��AudioSource(SE)�B�S�Ďg�p���̏ꍇ��null��ԋp</returns>
    private AudioSource GetUnusedSourceSE()
    {
        for (var i = 0; i < _seSources.Length; ++i)
        {
            if (!_seSources[i].isPlaying) return _seSources[i];
        }

        return null;
    }

    /// <summary>
    /// ���g�p��AudioSource(BGM)���������A�擾����֐�
    /// </summary>
    /// <returns>���g�p��AudioSource(BGM)�B�S�Ďg�p���̏ꍇ��null��ԋp</returns>
    private AudioSource GetUnusedSourceBGM()
    {
        for (var i = 0; i < _bgmSources.Length; ++i)
        {
            if (!_bgmSources[i].isPlaying) return _bgmSources[i];
        }

        return null;
    }

    /// <summary>
    /// ���O�Ŏw�肵��SE���Đ�����֐�
    /// </summary>
    /// <param seName="seName">SE��</param>
    public void PlaySE(string seName)
    {
        if (_seData.ContainsKey(seName))
        {
            if (Time.realtimeSinceStartup - _seData[seName].playedTime > INTERVAL) {
                var audioSource = GetUnusedSourceSE();

                if (audioSource)
                {
                    audioSource.clip = _seData[seName].audioClip;
                    audioSource.Play();
                    _seData[seName].playedTime = Time.realtimeSinceStartup;
                }
            }
        }
    }

    /// <summary>
    /// ���O�Ŏw�肵��BGM���Đ�����֐�
    /// </summary>
    /// <param bgmName="bgmName">BGM��</param>
    public void PlayBGM(string bgmName)
    {
        if (_bgmData.ContainsKey(bgmName))
        {
            if (Time.realtimeSinceStartup - _bgmData[bgmName].playedTime > INTERVAL)
            {
                var audioSource = GetUnusedSourceBGM();

                if (audioSource)
                {
                    audioSource.clip = _bgmData[bgmName].audioClip;
                    audioSource.Play();
                    _bgmData[bgmName].playedTime = Time.realtimeSinceStartup;
                }
            }
        }
    }

    /// <summary>
    /// BGM���~����֐�
    /// </summary>
    public void StopBGM(string bgmName)
    {
        foreach (var bgmSource in _bgmSources)
        {
            if(bgmSource.clip && bgmSource.clip.name == bgmName)
            {
                bgmSource.Stop();
            }
        }       
    }
}
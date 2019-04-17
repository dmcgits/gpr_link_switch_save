using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;
using Newtonsoft.Json;

public class SwappySprite : MonoBehaviour
{

    [SerializeField]
    private SpriteAtlas _spriteManager;
    private SpriteRenderer _renderer;

    private SpriteConfig _config;
    private string _savePath = "";

    struct SpriteConfig
    {
        public List<string> names;
        public int index;

        public SpriteConfig(List<string> spriteNames, int spriteIndex)
        {
            names = spriteNames;
            index = spriteIndex;
        }
    }

    private void Awake()
    {
        // keep a reference to the sprite renderer on this gameobject
        _savePath = Path.Combine(Application.persistentDataPath, "swappy_sprite_config.json");
        _renderer = gameObject.GetComponent<SpriteRenderer>();

        // Use var as the type so we don't have to type "List<string>" twice.
        // This is like the c++ "auto" keyword
        var names = new List<string>() { "spr_zelda", "spr_link", "spr_zelda_roar", "spr_link_hide" };

        _config = new SpriteConfig(names, 0);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        _renderer.sprite = _spriteManager.GetSprite(_config.names[_config.index]);
    }

    // Use this for initialization
    void Start()
    {
        // Listen for events
        SelectButton.OnSelect += OnSelectHandler;
        LoadButton.OnLoadRequested += OnLoadHandler;
        SaveButton.OnSaveRequested += OnSaveHandler;
    }
    private void OnSelectHandler()
    {
        //Debug.Log(gameObject.name + " OnSelectHandler");
        // Increment the sprite index. if it's gone past the end of the list, go to zero.
        if (++_config.index >= _config.names.Count) _config.index = 0;

        // Now change the sprite
        ChangeSprite();
    }

    /////// LOADING AND SAVING 

    private void OnSaveHandler()
    {
        string configJson = JsonConvert.SerializeObject(_config); // Convert (serialize) our objects to a JSon string
        Debug.Log("Saving configJson: " + configJson);

        File.WriteAllText(_savePath, configJson); // Save file
    }

    private void OnLoadHandler()
    {

        if (File.Exists(_savePath)) // Don't try to load a file that isn't there
        {
            string loadedJson = File.ReadAllText(_savePath); // read existing file

            SpriteConfig loadedConfig = JsonConvert.DeserializeObject<SpriteConfig>(loadedJson); // string back to sprite config object
                                                                                                 //charState[HAT] = charSaved[HAT];
            Debug.Log("loadedConfig sprite name = " + loadedConfig.names[loadedConfig.index]);
            _config.names = loadedConfig.names;
            _config.index = loadedConfig.index;
            ChangeSprite();
        }

    }
}

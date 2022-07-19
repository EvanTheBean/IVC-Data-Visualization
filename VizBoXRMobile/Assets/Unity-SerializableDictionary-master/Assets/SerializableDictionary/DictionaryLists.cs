using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListStorage : SerializableDictionary.Storage<List<string>> { }

[Serializable]
public class StringListDictionary : SerializableDictionary<string, List<string>, ListStorage> { }


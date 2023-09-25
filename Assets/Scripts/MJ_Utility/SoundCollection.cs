// Copyright (C) 2018-2019 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundCollection", menuName = "SoundSystem/Sound collection", order = 2)]
public class SoundCollection : ScriptableObject
{
	public List<AudioClip> Sounds;
}

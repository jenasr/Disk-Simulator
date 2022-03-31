using UnityEngine;
using System.Collections.Generic;


namespace YuGiOh {
    /// <summary>
    /// Lazy-loads each sprite and caches it
    /// </summary>
    public static class CardSpriteLoader {
        static Dictionary<int, Sprite> id2sprite = new Dictionary<int, Sprite>();

        public static Sprite Get(int id) {
            if (!id2sprite.ContainsKey(id)) {
                id2sprite.Add(id, Resources.Load<Sprite>($"Card Images/{id}"));
            }

            return id2sprite[id];
        }

        public static void Clear() {
            id2sprite = new Dictionary<int, Sprite>();
        }
    }

}
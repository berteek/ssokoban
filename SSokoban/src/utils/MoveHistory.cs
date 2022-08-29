using System.Collections.Generic;

using SFML.System;
using SSokoban.Core;
using SSokoban.EntitiesAndComponents;
using SSokoban.GameStates;

namespace SSokoban.Utils
{
    public class MoveRecord
    {
        public List<Entity> Entities { get; set; }
        public List<Vector2i> Directions { get; set; }

        public MoveRecord(List<Entity> entities, List<Vector2i> directions)
        {
            Entities = entities;
            Directions = directions;
        }
    }

    public static class MoveHistory
    {
        private static List<MoveRecord> moveHistory = new List<MoveRecord>();

        public static void Rewind()
        {
            RewindNet();
            Network.Send("rewind");
        }

        public static void CreateNewRecord(Entity entity, Vector2i direction)
        {
            moveHistory.Add(new MoveRecord(new List<Entity> { entity }, new List<Vector2i> { direction }));
            Network.Send("newrecord");
        }

        public static void CreateEmptyRecord()
        {
            moveHistory.Add(new MoveRecord(new List<Entity> { new Entity(new Vector2i(-1, -1)) }, new List<Vector2i> { new Vector2i(0, 0) }));
        }

        public static void AddToLastRecord(Entity entity, Vector2i direction)
        {
            moveHistory[moveHistory.Count - 1].Entities.Add(entity);
            moveHistory[moveHistory.Count - 1].Directions.Add(direction);
        }

        public static void ClearHistory()
        {
            moveHistory.Clear();
        }

        public static void RewindNet()
        {
            if (moveHistory.Count > 0)
            {
                MoveRecord moveRecord = moveHistory[moveHistory.Count - 1];
                for (int i = 0; i < moveRecord.Entities.Count; i++)
                {
                    moveRecord.Entities[i].Position -= moveRecord.Directions[i];
                }

                moveHistory.RemoveAt(moveHistory.Count - 1);
            }
        }
    }
}
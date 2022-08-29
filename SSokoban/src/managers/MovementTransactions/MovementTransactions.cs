using System.Linq;
using System.Collections.Generic;

using SSokoban.MapsAndSections;
using SSokoban.EntitiesAndComponents;
using SFML.System;

namespace SSokoban.Core
{
    public static class MovementTransactionSystem
    {
        public static List<MovementTransaction> Transactions { get; private set; } = new List<MovementTransaction>();

        public static Section Section { get; set; }

        public static void Process()
        {
            while (Transactions.Count > 0)
            {
                RemoveFailedTransactions();
                if (Transactions.Count != 0)
                    FinishTransaction();
            }
        }

        private static void FinishTransaction()
        {
            PushNextEntities(Transactions[0].Entity.Position, Transactions[0].Translation, Transactions[0].Entity.ZIndex);
            Transactions[0].Entity.Position += Transactions[0].Translation;
            Transactions.RemoveAt(0);
        }

        private static void PushNextEntities(Vector2i position, Vector2i translation, int ZIndex)
        {
            Entity nextEntity = Section.Entities.FirstOrDefault<Entity>
            (
                entity => entity.Position == position + translation
                        && entity.ZIndex == ZIndex
            );

            if (nextEntity != null)
            {
                PushNextEntities(position + translation, translation, ZIndex);
                nextEntity.Position += translation;
            }
        }

        private static void RemoveFailedTransactions()
        {
            List<MovementTransaction> transactionsToRemove = new List<MovementTransaction>();

            foreach (MovementTransaction transaction in Transactions)
            {
                bool isEnoughSpaceToMove = CheckIfEnoughSpaceToMove(transaction.Entity.Position, transaction.Translation, transaction.Entity.ZIndex);
                if (!isEnoughSpaceToMove)
                    transactionsToRemove.Add(transaction);
            }

            foreach (MovementTransaction transaction in transactionsToRemove)
            {
                Transactions.Remove(transaction);
            }
        }

        private static bool CheckIfEnoughSpaceToMove(Vector2i position, Vector2i translation, int ZIndex)
        {
            Entity entityAtPosition = Section.Entities.FirstOrDefault<Entity>(entity => entity.Position == position && entity.ZIndex == ZIndex);

            if (entityAtPosition == null)
                return true;

            MoveComponent entityAtPositionMoveComponent = entityAtPosition.GetComponent<MoveComponent>();

            if (entityAtPositionMoveComponent == null)
                return false;

            return CheckIfEnoughSpaceToMove(position + translation, translation, ZIndex);
        }

        public static void Submit(MovementTransaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
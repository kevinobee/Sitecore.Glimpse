using System;

using Sitecore.Data.Items;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Infrastructure
{
    public interface ISitecoreRepository
    {
        PatternCard[] GetPatternCards();
        bool IsGoal(Guid pageEventDefinitionId);
        Item GetItem(string itemId);
        Item GetItem(Guid itemId);
    }
}
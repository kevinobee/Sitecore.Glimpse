﻿using System;

using Sitecore.Data.Items;
using Sitecore.Glimpse.Model.Analytics;

namespace Sitecore.Glimpse.Infrastructure
{
    internal class CachingSitecoreRepository : ISitecoreRepository
    {
        private readonly ISitecoreRepository _wrappedRepository;

        private static PatternCard[] _patternCards;

        public CachingSitecoreRepository(ISitecoreRepository wrappedRepository)
        {
            _wrappedRepository = wrappedRepository;
        }

        public PatternCard[] GetPatternCards()
        {
            return _patternCards ?? (_patternCards = _wrappedRepository.GetPatternCards());
        }

        public bool IsGoal(Guid pageEventDefinitionId)
        {
            // TODO add caching to this call rather than pass thru
            return _wrappedRepository.IsGoal(pageEventDefinitionId);
        }

        public Item GetItem(string itemId)
        {
            return _wrappedRepository.GetItem(itemId);
        }

        public Item GetItem(Guid itemId)
        {
            return _wrappedRepository.GetItem(itemId);
        }
    }
}
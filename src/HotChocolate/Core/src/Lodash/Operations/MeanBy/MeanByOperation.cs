using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HotChocolate.Lodash
{
    public class MeanByOperation : AggregationOperation
    {
        public MeanByOperation(string key)
        {
            Key = key;
        }

        public string Key { get; }

        public override bool Rewrite(JsonNode? node, out JsonNode? rewritten)
        {
            if (node is null)
            {
                rewritten = null;
                return false;
            }

            if (node is JsonObject)
            {
                throw ThrowHelper.ExpectArrayButReceivedObject(node.GetPath());
            }

            if (node is JsonValue)
            {
                throw ThrowHelper.ExpectArrayButReceivedScalar(node.GetPath());
            }

            if (node is JsonArray arr)
            {
                rewritten = RewriteArray(arr);
                return true;
            }

            throw new ArgumentOutOfRangeException(nameof(node));
        }

        private double? RewriteArray(JsonArray value)
        {
            double divisor = 0;
            double sum = 0;

            foreach (JsonNode? element in value)
            {
                switch (element)
                {
                    case JsonArray:
                        throw ThrowHelper.ExpectObjectButReceivedArray(element.GetPath());
                    case JsonValue:
                        throw ThrowHelper.ExpectObjectButReceivedScalar(element.GetPath());
                    case JsonObject obj:
                    {
                        if (obj.TryGetPropertyValue(Key, out JsonNode? jsonNode) &&
                            jsonNode.TryConvertToNumber(out double converted))
                        {
                            sum += converted;
                            divisor++;
                        }

                        break;
                    }
                }
            }

            if (divisor == 0)
            {
                return null;
            }

            return sum / divisor;
        }
    }
}
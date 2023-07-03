namespace openAPI.Helper
{
    public static class ExtensionVectorOperation
    {
        public static float InnerProduct(this IEnumerable<float> vector1, IEnumerable<float> vector2)
        {
            return vector1.Zip(vector2, (a, b) => a * b).Sum();
        }

        public static float CosineSimilarity(this IEnumerable<float> vector1, IEnumerable<float> vector2)
        {
            return vector1.InnerProduct(vector2) / (float)(Math.Sqrt(vector1.InnerProduct(vector1)) * Math.Sqrt(vector2.InnerProduct(vector2)));
        }
    }
}

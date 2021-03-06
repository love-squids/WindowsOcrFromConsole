﻿using System.Collections.Generic;

namespace WindowsOcrWrapper.GoogleOcr
{
    public class GoogleOcrResponseMapper
    {
        internal static GoogleOcrResponse FromDynamic(dynamic googleResult)
        {
            GoogleOcrResponse googleOcrResponse = new GoogleOcrResponse();
            googleOcrResponse.Responses = new List<GoogleOcrSingleResponse>();
            foreach (var response in googleResult.responses)
            {
                var singleOcrResponse = new GoogleOcrSingleResponse();
                singleOcrResponse.Annotations = new List<GoogleTextAnnotation>();
                foreach (var textAnnotation in response.textAnnotations)
                {
                    var googleTextAnnotation = new GoogleTextAnnotation();
                    googleTextAnnotation.Description = textAnnotation.description;
                    googleTextAnnotation.Locale = textAnnotation.locale;
                    googleTextAnnotation.BoundingPoly = new GoogleBoundingPoly();
                    foreach (var boundingPoly in textAnnotation.boundingPoly)
                    {
                        foreach (var vertice in boundingPoly.Value)
                        {
                            googleTextAnnotation.BoundingPoly.Add(new GoogleVertice { X = vertice.x, Y = vertice.y });
                        }                        
                    }
                    singleOcrResponse.Annotations.Add(googleTextAnnotation);
                }
                googleOcrResponse.Responses.Add(singleOcrResponse);
            }
            return googleOcrResponse;
        }
    }
}


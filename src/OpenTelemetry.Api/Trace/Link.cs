// <copyright file="Link.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

#nullable enable

using System.Diagnostics;

namespace OpenTelemetry.Trace;

/// <summary>
/// Link associated with the span.
/// </summary>
public readonly struct Link : IEquatable<Link>
{
    internal readonly ActivityLink ActivityLink;

    /// <summary>
    /// Initializes a new instance of the <see cref="Link"/> struct.
    /// </summary>
    /// <param name="spanContext">Span context of a linked span.</param>
    public Link(in SpanContext spanContext)
        : this(in spanContext, attributes: null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Link"/> struct.
    /// </summary>
    /// <param name="spanContext">Span context of a linked span.</param>
    /// <param name="attributes">Link attributes.</param>
    public Link(in SpanContext spanContext, SpanAttributes? attributes)
    {
        this.ActivityLink = new ActivityLink(spanContext.ActivityContext, attributes?.Attributes);
    }

    /// <summary>
    /// Gets the span context of a linked span.
    /// </summary>
    public SpanContext Context
        => new(this.ActivityLink.Context);

    /// <summary>
    /// Gets the collection of attributes associated with the link.
    /// </summary>
    public IEnumerable<KeyValuePair<string, object?>>? Attributes
        => this.ActivityLink.Tags;

    /// <summary>
    /// Compare two <see cref="Link"/> for equality.
    /// </summary>
    /// <param name="link1">First link to compare.</param>
    /// <param name="link2">Second link to compare.</param>
    public static bool operator ==(Link link1, Link link2)
        => link1.Equals(link2);

    /// <summary>
    /// Compare two <see cref="Link"/> for not equality.
    /// </summary>
    /// <param name="link1">First link to compare.</param>
    /// <param name="link2">Second link to compare.</param>
    public static bool operator !=(Link link1, Link link2)
        => !link1.Equals(link2);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is Link link && this.ActivityLink.Equals(link.ActivityLink);

    /// <inheritdoc />
    public override int GetHashCode()
        => this.ActivityLink.GetHashCode();

    /// <inheritdoc/>
    public bool Equals(Link other)
        => this.ActivityLink.Equals(other.ActivityLink);
}

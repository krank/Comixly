using System;

public readonly record struct ComicBook(
  string title,
  string subtitle,
  bool drmFree
);
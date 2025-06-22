# -- Project information -----------------------------------------------------
project = 'sqs-project'
copyright = '2025, Lucas Weiss'
author = 'Lucas Weiss'

# -- General configuration ---------------------------------------------------
extensions = ['myst_parser']

source_suffix = {
    '.rst': 'restructuredtext',
    '.md': 'markdown',
}

templates_path = ['_templates']
exclude_patterns = []

# -- Options for HTML output -------------------------------------------------
html_theme = 'sphinx_rtd_theme'
html_static_path = ['_static']

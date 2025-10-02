#!/bin/sh
set -e

CERT_DIR="/certs/deployment/ca"
DEST_DIR="/usr/local/share/ca-certificates"

echo ">> Installing CA certs from $CERT_DIR ..."

# Convert everything into .crt PEM format (overwrite if needed)
for file in "$CERT_DIR"/*; do
    name=$(basename "$file")
    openssl x509 -in "$file" -out "$DEST_DIR/$name.crt" -inform DER 2>/dev/null || \
    cp "$file" "$DEST_DIR/$name.crt"
done

echo ">> Updating system certificates..."
update-ca-certificates

echo ">> Done."